# Safe Share

Safe Share is a full-stack file-sharing application built with React, ASP.NET Core, PostgreSQL, and MinIO.

## Project structure

```text
safe-share/
├── safe-share-frontend/   # React + Vite frontend
├── safe-share-backend/    # ASP.NET Core API, EF Core, and Docker Compose
└── README.md
```

## Prerequisites

Install the following tools:

- Node.js 20 or newer
- npm
- .NET SDK 10.0 or newer
- Docker Engine
- Docker Compose

Verify the installations:

```bash
node --version
npm --version
dotnet --version
docker --version
docker compose version
```

## Configuration

### Frontend environment

Create `safe-share-frontend/.env`:

```env
VITE_API_URL=http://localhost:8080/api
```

The frontend uses this value for API requests. The `.env` file is local configuration and should not be committed.

### Backend configuration

The API runs inside Docker and connects to the Compose services using their service names:

```text
PostgreSQL: Host=db;Port=5432
MinIO:      http://minio:9000
```

The development database configuration is:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=db;Port=5432;Database=safesharedb;Username=postgres;Password=123456"
}
```

The MinIO development credentials are configured by `safe-share-backend/compose.yaml` and used by the backend:

```text
Endpoint:  http://minio:9000
Access key: admin
Secret key: SuperSecret123!
Bucket:     safeshare-files
```

Do not use these development credentials in production.

## Start the project

### 1. Install frontend dependencies

From the repository root:

```bash
cd safe-share-frontend
npm install
```

Create the frontend environment file if it does not exist:

```bash
printf 'VITE_API_URL=http://localhost:8080/api\n' > .env
```

### 2. Start the backend, PostgreSQL, and MinIO

Open a second terminal and run:

```bash
cd safe-share-backend
docker compose up -d --build
```

Check the services:

```bash
docker compose ps
```

The services are exposed at:

| Service | Address |
|---|---|
| API | http://localhost:8080 |
| PostgreSQL | localhost:5433 |
| MinIO S3 API | http://localhost:9000 |
| MinIO console | http://localhost:9001 |

Open the MinIO console at [http://localhost:9001](http://localhost:9001):

```text
Username: admin
Password: SuperSecret123!
```

The API applies Entity Framework migrations and creates the `safeshare-files` bucket during startup.

### 3. Start the frontend

From the frontend directory:

```bash
cd safe-share-frontend
npm run dev -- --host 0.0.0.0
```

Open the application at:

```text
http://localhost:5173
```

## Stop the project

Stop the containers but keep database and object-storage data:

```bash
cd safe-share-backend
docker compose down
```

Remove the containers and their named volumes, including all local database and MinIO data:

```bash
docker compose down -v
```

## Run the API without Docker

The normal development setup runs the API in Docker because the configured database and MinIO endpoints use Compose service names. To run the API directly on the host, change the connection and storage endpoints to host addresses first:

```text
PostgreSQL: Host=localhost;Port=5433
MinIO:      http://localhost:9000
```

Then run:

```bash
cd safe-share-backend/SafeShare.Api
dotnet restore
dotnet run
```

When using the HTTP launch profile, the API is available at the launch URL shown by `dotnet run`. The frontend `.env` must point to that URL.

## Useful commands

Frontend:

```bash
cd safe-share-frontend
npm run dev
npm run build
npm run lint
npm run preview
```

Backend:

```bash
cd safe-share-backend
dotnet build SafeShare.slnx
docker compose logs -f safeshare.api
docker compose logs -f minio
docker compose restart safeshare.api
```

## Troubleshooting

### API container exits

Check the API logs:

```bash
cd safe-share-backend
docker compose logs safeshare.api
```

Confirm that:

- PostgreSQL is healthy;
- MinIO is running;
- the connection string uses `Host=db`, `Port=5432`, and password `123456`;
- the API is being started through Docker rather than a host process using Docker-only hostnames.

### Frontend cannot reach the API

Confirm `safe-share-frontend/.env` contains:

```env
VITE_API_URL=http://localhost:8080/api
```

Restart Vite after changing `.env`, because Vite loads environment variables when it starts.

### MinIO data or bucket is missing

The named `minio_data` volume persists objects between restarts. Removing it with `docker compose down -v` permanently deletes local MinIO data. The API recreates the `safeshare-files` bucket on the next startup.

## TLS for local MinIO

The default Compose configuration uses HTTP for local development. If HTTPS is required, generate a local certificate from the repository root:

```bash
mkdir -p safe-share-backend/minio/certs

openssl req \
  -x509 \
  -newkey rsa:2048 \
  -sha256 \
  -nodes \
  -days 365 \
  -keyout safe-share-backend/minio/certs/private.key \
  -out safe-share-backend/minio/certs/public.crt \
  -subj "/CN=localhost" \
  -addext "subjectAltName=DNS:localhost,IP:127.0.0.1,DNS:minio"
```

The certificate files are local secrets/configuration and must not be committed, especially `private.key`. The certificate directory contains a `.gitkeep` placeholder so the directory can exist in a fresh checkout.

TLS also requires the MinIO container to mount `./minio/certs` at `/root/.minio/certs`, and the public storage URL used to generate browser-facing URLs must be changed to `https://localhost:9000`. After changing the Compose or backend configuration, recreate the containers:

```bash
cd safe-share-backend
docker compose down
docker compose up -d --build --force-recreate
```

Because this certificate is self-signed, open `https://localhost:9000/minio/health/live` in the browser and accept the local certificate warning before testing browser uploads.

## Security notes

- The credentials in this README and Compose file are for local development only.
- Never commit private TLS keys, production credentials, JWT secrets, or real connection strings.
- Use a trusted certificate authority and secret management in production.
