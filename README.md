# hostman

This is a host configuration management service. It is capable of managing
multiple users and their different hosts.

## Interface

The service's frontend is a RESTful API that may be accessed by client
applications. Each HTTP request to the frontend is required to include a
JWT as value of the Authorization header that will be used for authenticating
the requester via OpenID Connect.

The backend of the service is the (MySQL) database that is used for storing the
persistent data (like user information or host configuration).
