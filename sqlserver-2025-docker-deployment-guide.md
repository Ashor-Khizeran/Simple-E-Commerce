1. Overview
This guide explains how to download, deploy, configure, and access SQL Server 2025 using Docker. It also includes a detailed breakdown of all available MSSQL_PID editions.

2. Prerequisites
Docker Engine 1.8+

Minimum 2 GB RAM

Strong SA password (uppercase + lowercase + number + symbol)

3. Download the SQL Server 2025 Image
Pull the latest stable release:

bash
docker pull mcr.microsoft.com/mssql/server:2025-latest
Or pull a specific cumulative update:

bash
docker pull mcr.microsoft.com/mssql/server:2025-CU1-ubuntu-24.04
4. Deploy the SQL Server 2025 Container
Run the container with required environment variables:

bash
docker run -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrongPassword!" \
  -e "MSSQL_PID=Evaluation" \
  -p 1433:1433 \
  --name sql2025 \
  --hostname sql2025 \
  -d mcr.microsoft.com/mssql/server:2025-latest
Environment Variables
ACCEPT_EULA — must be Y

MSSQL_SA_PASSWORD — strong password required

MSSQL_PID — edition selector (explained in detail below)

5. Verify the Container
bash
docker ps
You should see sql2025 running.

6. Access SQL Server Inside the Container
SQL Server 2025 uses mssql-tools18:

bash
docker exec -it sql2025 /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P YourStrongPassword!
Test the connection:

sql
SELECT @@VERSION;
GO
7. Access SQL Server From Your Host
Using sqlcmd:
bash
sqlcmd -S localhost -U sa -P YourStrongPassword!
Using Azure Data Studio / SSMS:
Server: localhost

Authentication: SQL Login

Username: sa

Password: YourStrongPassword!

8. Create a Database
sql
CREATE DATABASE InventoryDB;
GO
9. List Databases
sql
SELECT name FROM sys.databases;
GO
10. Stop or Remove the Container
Stop:

bash
docker stop sql2025
Remove:

bash
docker rm sql2025
🧩 SQL Server Editions Explained (MSSQL_PID Values)
Below is a clear, practical explanation of each edition, focusing on what matters for development, testing, and production.

Evaluation
Purpose: Full-feature trial edition

180‑day time‑limited

Includes all Enterprise features

Ideal for testing high‑end features (HA, replication, advanced performance)

Not licensed for production

Best for: short-term testing, proof-of-concept, performance evaluation

Express
Purpose: Free lightweight edition

Max 1 GB memory per database engine instance

Max 10 GB per database

No SQL Agent

Great for small apps, demos, or learning

Best for: small apps, local dev, training, tiny workloads

StandardDeveloper
Purpose: Free developer edition with Standard feature set

Same features as Standard

Not licensed for production

Good for teams who want to simulate Standard environments

Best for: dev/test environments matching Standard edition

EnterpriseDeveloper
Purpose: Free developer edition with Enterprise feature set

Same features as Enterprise

Not licensed for production

Includes advanced features:

Always On

In‑Memory OLTP

Columnstore

Advanced security

Best for: full enterprise-level development, architectural testing

Standard
Purpose: Mid‑tier production edition

Supports most business workloads

Includes SQL Agent

Supports basic HA

Does not include advanced Enterprise features

Best for: small to medium businesses, general production workloads

Enterprise
Purpose: High-end production edition (per-server licensing)

Limited to 20 physical cores / 40 hyperthreaded cores

Includes all advanced features

Best for: large workloads, mission-critical systems, high availability

EnterpriseCore
Purpose: High-end production edition (per-core licensing)

Uses all OS cores

No core limit

Includes all Enterprise features

Best for: massive workloads, cloud-scale, heavy OLTP, analytics
