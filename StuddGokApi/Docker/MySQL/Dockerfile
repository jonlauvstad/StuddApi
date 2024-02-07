FROM mysql:8.0.33

ENV MYSQL_ROOT_PASSWORD=gokstad

COPY make_database.sql /docker-entrypoint-initdb.d/
COPY user_privileges.sql /docker-entrypoint-initdb.d/
COPY z_BigTableLetter.sql /docker-entrypoint-initdb.d/

EXPOSE 3306 