# NameBright API Examples

Example scripts for calling the NameBright Public API.

**API documentation: <https://api.namebright.com/documentation>**

The OpenAPI spec is at <https://api.namebright.com/documentation/current/doc.json>.

## Before you start

Request access to the NameBright API on [my.namebright.com](https://my.namebright.com/my-account/api-management) and wait for your request to be approved. If you plan to make purchases using the API, ensure your account has a funded balance. Then, create a new client and set the level of access it should have.

## Authentication

A token for API use can be obtained using your client ID and secret. Tokens can only be requested from IPv4 addresses your client is allowed to use. Anything else gets a `403`.

Request an access token from `POST https://api.namebright.com/auth/token`:

```sh
curl -X POST https://api.namebright.com/auth/token \
  -H "Content-Type: application/json" \
  -d '{
    "grant_type": "client_credentials",
    "client_id": "nb:example:myapiclient",
    "client_secret": "your-client-secret"
  }'
```

The endpoint also accepts camelCase JSON keys, or a form-encoded body.

## Making a call

Send the token as a bearer token. For example, to get your account details:

```sh
curl https://api.namebright.com/account \
  -H "Authorization: Bearer $TOKEN"
```

The response contains a `wallet` object with the account's payment methods. The older top-level `accountBalance` field is deprecated in favor of the funded balance inside `wallet`.

## Scripts

| File | What it does |
| --- | --- |
| `account.sh` | Gets a token and prints the account details |

```sh
export NB_CLIENT_ID=your-client-id
export NB_CLIENT_SECRET=your-client-secret
./account.sh
```