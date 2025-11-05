for db in $(psql -U postgres -Atc "SELECT datname FROM pg_database WHERE datistemplate = false;"); do
  echo "=> $db"
  psql -U postgres -d "$db" -c "CREATE EXTENSION IF NOT EXISTS pg_stat_statements;"
done
