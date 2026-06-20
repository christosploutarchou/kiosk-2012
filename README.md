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


d. ALTER TABLE USERS
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT USERS_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE USERS
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

e. ALTER TABLE SESSIONS
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT SESSIONS_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE SESSIONS
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

f. ALTER TABLE LOTTERY
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT LOTTERY_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE LOTTERY
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;



h. ALTER TABLE VAT_TYPES
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT VAT_TYPES_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE VAT_TYPES
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

-- PRODUCTS
ALTER TABLE PRODUCTS
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT PRODUCTS_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE PRODUCTS
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE PRODUCTS
ADD UUID VARCHAR2(32);

UPDATE PRODUCTS
SET UUID = LOWER(RAWTOHEX(SYS_GUID()))
WHERE UUID IS NULL;

COMMIT;

ALTER TABLE PRODUCTS
MODIFY UUID NOT NULL;

ALTER TABLE PRODUCTS
ADD CONSTRAINT PRODUCTS_UUID_UK
UNIQUE (UUID);

ALTER TABLE BARCODES
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT BARCODES_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

-- BARCODES
ALTER TABLE BARCODES
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE BARCODES
ADD PRODUCT_UUID VARCHAR2(32);

UPDATE BARCODES B
SET PRODUCT_UUID =
(
    SELECT P.UUID
    FROM PRODUCTS P
    WHERE P.SERNO = B.PRODUCT_SERNO
);

COMMIT;

ALTER TABLE BARCODES
ADD CONSTRAINT BARCODES_FK_PRODUCT_UUID
FOREIGN KEY (PRODUCT_UUID)
REFERENCES PRODUCTS(UUID);

CREATE INDEX IDX_BARCODES_PRODUCT_UUID
ON BARCODES(PRODUCT_UUID);

--
--TODO: Barcodes H. Switch Primary Key

Only after ALL references are migrated.

Check:

SELECT *
FROM USER_CONSTRAINTS
WHERE R_CONSTRAINT_NAME =
(
    SELECT CONSTRAINT_NAME
    FROM USER_CONSTRAINTS
    WHERE TABLE_NAME='PRODUCTS'
    AND CONSTRAINT_TYPE='P'
);

This shows every FK referencing PRODUCTS.

When all FKs are moved:

Drop old PK:

ALTER TABLE PRODUCTS
DROP PRIMARY KEY;

Create new PK:

ALTER TABLE PRODUCTS
ADD CONSTRAINT PRODUCTS_PK
PRIMARY KEY (UUID);
I. Keep SERNO

I strongly recommend keeping:

SERNO NUMBER(10)

with a unique constraint:

ALTER TABLE PRODUCTS
ADD CONSTRAINT PRODUCTS_SERNO_UK
UNIQUE (SERNO);
 -----------

-- SUPPLIERS 
ALTER TABLE SUPPLIERS
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT SUPPLIERS_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE SUPPLIERS
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

k. ALTER TABLE CATEGORIES
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT CATEGORIES_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE CATEGORIES
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

-- BUTTONS, ADD KIOSKID
BEGIN

    FOR i IN 1..23 LOOP

        BEGIN
            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i ||
                ' ADD (KIOSKID VARCHAR2(32))';

            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i ||
                ' ADD CONSTRAINT BTN_POS' || i || '_FK_KIOSK
                   FOREIGN KEY (KIOSKID)
                   REFERENCES KIOSK(KIOSKID)';
        EXCEPTION
            WHEN OTHERS THEN
                IF SQLCODE <> -1430 THEN
                    RAISE;
                END IF;
        END;

        BEGIN
            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i || '_DET
                 ADD (KIOSKID VARCHAR2(32))';

            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i || '_DET
                 ADD CONSTRAINT BTN_POS' || i || '_DET_FK_KIOSK
                 FOREIGN KEY (KIOSKID)
                 REFERENCES KIOSK(KIOSKID)';
        EXCEPTION
            WHEN OTHERS THEN
                IF SQLCODE <> -1430 THEN
                    RAISE;
                END IF;
        END;

    END LOOP;

END;
/

-- ADD PRODUCT_UUID
BEGIN

    FOR i IN 1..23 LOOP

        ------------------------------------------------------------------
        -- Add PRODUCT_UUID column
        ------------------------------------------------------------------
        BEGIN
            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i || '_DET
                 ADD (PRODUCT_UUID VARCHAR2(32))';
        EXCEPTION
            WHEN OTHERS THEN
                IF SQLCODE <> -1430 THEN -- column already exists
                    RAISE;
                END IF;
        END;

        ------------------------------------------------------------------
        -- Populate PRODUCT_UUID
        ------------------------------------------------------------------
        EXECUTE IMMEDIATE
            'UPDATE BTN_POS' || i || '_DET d
               SET PRODUCT_UUID =
                   (
                       SELECT p.UUID
                         FROM PRODUCTS p
                        WHERE p.SERNO = d.PRODUCT_SERNO
                   )
             WHERE PRODUCT_SERNO IS NOT NULL';

        ------------------------------------------------------------------
        -- Create index
        ------------------------------------------------------------------
        BEGIN
            EXECUTE IMMEDIATE
                'CREATE INDEX IDX_BTN_POS' || i || '_DET_PRODUCT_UUID
                   ON BTN_POS' || i || '_DET(PRODUCT_UUID)';
        EXCEPTION
            WHEN OTHERS THEN
                IF SQLCODE <> -955 THEN -- index already exists
                    RAISE;
                END IF;
        END;

        ------------------------------------------------------------------
        -- Foreign key
        ------------------------------------------------------------------
        BEGIN
            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i || '_DET
                 ADD CONSTRAINT BTN_POS' || i || '_DET_FK_PRODUCT_UUID
                 FOREIGN KEY (PRODUCT_UUID)
                 REFERENCES PRODUCTS(UUID)';
        EXCEPTION
            WHEN OTHERS THEN
                IF SQLCODE <> -2261 THEN
                    RAISE;
                END IF;
        END;

    END LOOP;

    COMMIT;

END;
/

-- Populate kioskid
BEGIN

    FOR i IN 1..23 LOOP

        EXECUTE IMMEDIATE
            'UPDATE BTN_POS' || i || '
             SET KIOSKID = ''55F3FEB197474ECAA84867CFB8CD8CC6''
             WHERE KIOSKID IS NULL';

        EXECUTE IMMEDIATE
            'UPDATE BTN_POS' || i || '_DET
             SET KIOSKID = ''55F3FEB197474ECAA84867CFB8CD8CC6''
             WHERE KIOSKID IS NULL';

      COMMIT;
    END LOOP; 
END;
/

-- UPDATED_AT
BEGIN

    FOR i IN 1..23 LOOP

        ------------------------------------------------------------------
        -- BTN_POSx
        ------------------------------------------------------------------
        BEGIN
            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i ||
                ' ADD (UPDATED_AT TIMESTAMP(6) DEFAULT CURRENT_TIMESTAMP)';
        EXCEPTION
            WHEN OTHERS THEN
                IF SQLCODE <> -1430 THEN -- column already exists
                    RAISE;
                END IF;
        END;

        ------------------------------------------------------------------
        -- BTN_POSx_DET
        ------------------------------------------------------------------
        BEGIN
            EXECUTE IMMEDIATE
                'ALTER TABLE BTN_POS' || i || '_DET
                 ADD (UPDATED_AT TIMESTAMP(6) DEFAULT CURRENT_TIMESTAMP)';
        EXCEPTION
            WHEN OTHERS THEN
                IF SQLCODE <> -1430 THEN
                    RAISE;
                END IF;
        END;

    END LOOP;

END;
/

-------
Every table that users can edit should contain these columns:

UPDATED_AT
SYNC_STATUS

where

0 = Synced
1 = Pending Upload
2 = Upload Failed

Whenever your application inserts or updates locally:

UPDATED_AT = CURRENT_TIMESTAMP
SYNC_STATUS = 1

After Oracle accepts the change:

SYNC_STATUS = 0

--TODO: SYNC SESSIONS