CREATE USER pockybot WITH PASSWORD '<PASSWORD>';
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO pockybot;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO pockybot;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO pockybot;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO pockybot;
