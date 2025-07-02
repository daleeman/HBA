# HBA
House Broker Application


"DefaultConnection": "Server=DESKTOP-UL3EADV\\SQLEXPRESS;Database=HBA;Trusted_Connection=True;TrustServerCertificate=True;"


broker@hba.com
Broker123!

seeker@hba.com
Seeker123!
--------------------------------------------------------------------------
Multi-Layered Architecture following Repository pattern

HBA.Data //this layer contains the definition all the models/entities
HBA.DataAccess //this layer establishes connection with database using EFCore
HBA.Infrastructure //this layer is the repository/service layer
HBA.Web //this layer is the main core MVC contains all business logics
--------------------------------------------------------------------------
Tables

dbo.Property 
PK Id (int)
FK PropertyTypeId (int)
PropertyName (nvarchar)
Location (nvarchar)
Price (decimal)

dbo.PropertyType
PK Id (int)
Type (nvarchar)

dbo.CommissionSetup
Pk Id (int)
FromAmount (decimal)
ToAmount (decimal)
CommissionValue (decimal)

Identity Tables for Authentication and Authorisation
