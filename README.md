# kiosk

User action
   ↓
SQLite (always write here first)
   ↓
Sync service
   ↓
Oracle RDS (eventual consistency)

App Starts
    |
    +-- Open SQLite
    |
    +-- Create Tables If Needed
    |
    +-- Oracle Reachable?
           |
           +-- Yes
           |      |
           |      +-- First Run?
           |              |
           |              +-- Full Download
           |              |
           |              +-- No -> Incremental Sync
           |
           +-- No
                  |
                  +-- Use Existing SQLite Data


SQLITE MIGRATION STEPS:

a. C:/sqlite.txt
b. 
CREATE TABLE KIOSK 
(
  KIOSKID VARCHAR2(32) PRIMARY KEY,
  DESCRIPTION VARCHAR2(32)
);

c. ALTER TABLE GLOBAL_PARAMS
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT GP_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE GLOBAL_PARAMS
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;