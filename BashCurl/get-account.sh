#!/usr/bin/env bash
# Get an access token from the NameBright Public API and print the account details.
# Requires: curl, jq
set -euo pipefail

: "${NB_CLIENT_ID:?Set NB_CLIENT_ID to your API client ID}"
: "${NB_CLIENT_SECRET:?Set NB_CLIENT_SECRET to your API client secret}"

api=https://api.namebright.com

# Run curl and print the response body. On a non-2xx status, print the
# status and body to stderr instead and return non-zero.
request() {
  local out status body
  out=$(curl -sS -w '\n%{http_code}' "$@")
  status=${out##*$'\n'}
  body=${out%$'\n'*}
  if [ "$status" -lt 200 ] || [ "$status" -ge 300 ]; then
    echo "HTTP $status" >&2
    printf '%s\n' "$body" >&2
    return 1
  fi
  printf '%s\n' "$body"
}

body=$(jq -n \
  --arg id "$NB_CLIENT_ID" \
  --arg secret "$NB_CLIENT_SECRET" \
  '{grant_type: "client_credentials", client_id: $id, client_secret: $secret}')

token_response=$(request -X POST "$api/auth/token" \
  -H "Content-Type: application/json" \
  -d "$body")

token=$(jq -r '.access_token // empty' <<< "$token_response")

if [ -z "$token" ]; then
  echo "No access_token in response: $token_response" >&2
  exit 1
fi

request "$api/account" -H "Authorization: Bearer ${token}" | jq .