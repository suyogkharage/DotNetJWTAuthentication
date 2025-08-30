sequenceDiagram
    participant Client
    participant Server
    participant Database

    Client->>Server: POST /login <br/> { username, password }
    Server->>Database: Validate credentials
    Database-->>Server: User found + valid password
    Server-->>Client: Return JWT <br/> { header.payload.signature }

    Note over Client: Stores token (localStorage, cookie, etc.)

    Client->>Server: GET /protected <br/> Authorization: Bearer <token>
    Server->>Server: Decode JWT header & payload
    Server->>Server: Verify signature with secret/public key
    alt Signature valid
        Server->>Server: Check claims (iss, aud, exp, role)
        alt Claims valid
            Server-->>Client: 200 OK <br/> Protected resource
        else Claims invalid (expired, wrong aud/iss)
            Server-->>Client: 401 Unauthorized <br/> "Invalid token claims"
        end
    else Signature invalid
        Server-->>Client: 401 Unauthorized <br/> "Invalid signature"
    end
