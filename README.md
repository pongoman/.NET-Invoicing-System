# .NET-Invoicing-System


ASP.NET application - reporting system 


 * Build with ASP.NET CORE 3.0
 * MARIADB v10.6
 * EntityFramework

The application reads the database tables which hold invoices and details of them, and enable the user to export reports in .CSV files and update the payment status.
For more information on the data, take a look at the models structure and the data context files.

## Run Instructions

**Database**

* Download and Install Maria DB to work in a local enviroment.
* Create a database and user with privileges

Create a new database. You can use the MySQL command line for MariaDB or a tool like DBeaver.
```
CREATE DATABASE myDB;
```
Access the list of DBs.

```
SHOW DATABASES;
```

Create a new user with password for your database.

```
CREATE USER 'user2'@localhost IDENTIFIED BY 'password2;
```

Check if user is created.
```
SELECT User FROM mysql.user;
```

Grant all privileges to the user.
```
GRANT ALL PRIVILEGES ON *.* TO 'user2'@localhost IDENTIFIED BY 'password2;
```

If refresh of privileges is needed.
```
FLUSH PRIVILEGES;
```

Check privileges.
```
SHOW GRANTS FOR 'user2'@localhost;
```

If you want to remove user.

```
DROP USER 'user2'@localhost;
```

* Seed database using .sql file
* Configure connection string

For example:
```
"ConnectionStrings": {
    "InvoiceSystem": "server=localhost;port=3306;uid=user2;pwd=password2;database=myDB"
  },
```


Run the app (IIS Express).
