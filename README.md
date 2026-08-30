# Safe Share

Safe Share is a full-stack application with:
- React + Vite frontend
- ASP.NET Core backend
- PostgreSQL database
- MinIO object storage

## Project structure

```text
safe-share/
├── safe-share-frontend/     # React app
├── safe-share-backend/      # .NET backend and infrastructure
├── README.md               # this file
└── .gitignore
```

## Prerequisites

Before running the project, install:
- Node.js 20+
- npm
- .NET SDK 10.0+
- Docker + Docker Compose

## 1) Install frontend dependencies

From the frontend folder:

```bash
cd safe-share-frontend
npm install
```

Create or update the frontend environment file:

```bash
cat > .env <<'EOF'
VITE_API_URL=http://localhost:8080/api
EOF
```

## 2) Start the backend services

From the backend folder:

```bash
cd safe-share-backend
docker compose up -d
```

This starts:
- PostgreSQL on `localhost:5433`
- MinIO on `localhost:9000` and `localhost:9001`
- the API container on `localhost:8080`

If you want to run the API directly instead of via Docker, use:

```bash
cd safe-share-backend
cd SafeShare.Api
dotnet restore
dotnet run
```

The API should listen on `http://localhost:8080` when running through Docker.

## 3) Run the frontend

From the frontend folder:

```bash
cd safe-share-frontend
npm run dev -- --host 0.0.0.0
```

Then open:

```text
http://localhost:5173
```

## 4) Build the frontend for production

```bash
cd safe-share-frontend
npm run build
```

To preview the production build:

```bash
npm run preview -- --host 0.0.0.0
```

## 5) Backend notes

The backend uses:
- ASP.NET Core Web API
- Entity Framework Core
- JWT authentication
- Wolverine for command handling
- PostgreSQL as primary database
- MinIO for file/object storage

Database migrations are applied automatically on startup.

## Common issues

- If requests fail, check the API URL in `safe-share-frontend/.env`.
- Confirm Docker containers are running with:

```bash
docker compose ps
```

- If the API is not responding, ensure the backend is up and the port `8080` is exposed.

## Useful commands

Frontend:
```bash
cd safe-share-frontend
npm install
npm run dev
npm run build
```

Backend:
```bash
cd safe-share-backend
docker compose up -d
docker compose down
```

## License

This project does not include a license file yet. Add one if you plan to publish the project.
