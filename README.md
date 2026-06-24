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

DECLARE
    v_count NUMBER;
BEGIN

    ------------------------------------------------------------------
    -- ADD UUID COLUMN
    ------------------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME = 'RECEIPTS'
    AND COLUMN_NAME = 'UUID';

    IF v_count = 0 THEN
        EXECUTE IMMEDIATE
        'ALTER TABLE RECEIPTS ADD UUID VARCHAR2(32)';
    END IF;


    ------------------------------------------------------------------
    -- POPULATE EXISTING RECEIPTS
    ------------------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    UPDATE RECEIPTS
       SET UUID = RAWTOHEX(SYS_GUID())
     WHERE UUID IS NULL
    ';


    ------------------------------------------------------------------
    -- MAKE UUID NOT NULL
    ------------------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    ALTER TABLE RECEIPTS MODIFY UUID NOT NULL
    ';


    ------------------------------------------------------------------
    -- ADD UNIQUE CONSTRAINT
    ------------------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_CONSTRAINTS
    WHERE TABLE_NAME = 'RECEIPTS'
    AND CONSTRAINT_NAME = 'UK_RECEIPTS_UUID';

    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE RECEIPTS
        ADD CONSTRAINT UK_RECEIPTS_UUID
        UNIQUE(UUID)
        ';

    END IF;


    ------------------------------------------------------------------
    -- ADD KIOSKID IF MISSING
    ------------------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME = 'RECEIPTS'
    AND COLUMN_NAME = 'KIOSKID';

    IF v_count = 0 THEN
        EXECUTE IMMEDIATE
        '
        ALTER TABLE RECEIPTS
        ADD KIOSKID VARCHAR2(32)
        ';
    END IF;


    ------------------------------------------------------------------
    -- ADD UPDATED_AT IF MISSING
    ------------------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME = 'RECEIPTS'
    AND COLUMN_NAME = 'UPDATED_AT';

    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE RECEIPTS
        ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ';

    END IF;


    ------------------------------------------------------------------
    -- INITIALIZE UPDATED_AT
    ------------------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    UPDATE RECEIPTS
       SET UPDATED_AT = CREATED_ON
     WHERE UPDATED_AT IS NULL
    ';


    COMMIT;

END;
/

CREATE INDEX IDX_RECEIPTS_SYNC
ON RECEIPTS(KIOSKID, UPDATED_AT);

CREATE INDEX IDX_RECEIPTS_UUID
ON RECEIPTS(UUID);


-- RECEIPTS_DET
DECLARE

    v_count NUMBER;

BEGIN

    ---------------------------------------------------------
    -- ADD RECEIPT_UUID
    ---------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='RECEIPTS_DET'
    AND COLUMN_NAME='RECEIPT_UUID';

    IF v_count = 0 THEN
        EXECUTE IMMEDIATE
        '
        ALTER TABLE RECEIPTS_DET
        ADD RECEIPT_UUID VARCHAR2(32)
        ';
    END IF;


    ---------------------------------------------------------
    -- ADD PRODUCT_UUID
    ---------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='RECEIPTS_DET'
    AND COLUMN_NAME='PRODUCT_UUID';

    IF v_count = 0 THEN
        EXECUTE IMMEDIATE
        '
        ALTER TABLE RECEIPTS_DET
        ADD PRODUCT_UUID VARCHAR2(32)
        ';
    END IF;


    ---------------------------------------------------------
    -- ADD KIOSKID
    ---------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='RECEIPTS_DET'
    AND COLUMN_NAME='KIOSKID';

    IF v_count = 0 THEN
        EXECUTE IMMEDIATE
        '
        ALTER TABLE RECEIPTS_DET
        ADD KIOSKID VARCHAR2(32)
        ';
    END IF;


    ---------------------------------------------------------
    -- ADD UPDATED_AT
    ---------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='RECEIPTS_DET'
    AND COLUMN_NAME='UPDATED_AT';

    IF v_count = 0 THEN
        EXECUTE IMMEDIATE
        '
        ALTER TABLE RECEIPTS_DET
        ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ';
    END IF;


END;
/

MERGE /*+ PARALLEL(4) */
INTO RECEIPTS_DET d

USING
(
    SELECT
        SERNO,
        UUID,
        KIOSKID
    FROM RECEIPTS
) r

ON
(
    d.RECEIPT_SERNO = r.SERNO
)

WHEN MATCHED THEN

UPDATE SET

d.RECEIPT_UUID = r.UUID,
d.KIOSKID = r.KIOSKID

WHERE d.RECEIPT_UUID IS NULL;

COMMIT;

MERGE /*+ PARALLEL(4) */
INTO RECEIPTS_DET d

USING
(
    SELECT
        SERNO,
        UUID
    FROM PRODUCTS
) p

ON
(
    d.PRODUCT_SERNO = p.SERNO
)

WHEN MATCHED THEN

UPDATE SET

d.PRODUCT_UUID = p.UUID

WHERE d.PRODUCT_UUID IS NULL;

COMMIT;

ALTER TABLE RECEIPTS_DET
ADD UUID VARCHAR2(32);

UPDATE /*+ PARALLEL(4) */ RECEIPTS_DET
SET UUID = RAWTOHEX(SYS_GUID())
WHERE UUID IS NULL;

COMMIT;

ALTER TABLE RECEIPTS_DET
MODIFY UUID NOT NULL;

ALTER TABLE RECEIPTS_DET
ADD CONSTRAINT UK_RECEIPTS_DET_UUID
UNIQUE(UUID);

CREATE INDEX IDX_RECEIPTS_DET_UUID
ON RECEIPTS_DET(UUID);


-- X_REPORT
ALTER TABLE X_REPORT
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT X_REPORT_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE X_REPORT
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

-- Z_REPORT
ALTER TABLE Z_REPORT
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT Z_REPORT_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

ALTER TABLE Z_REPORT
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

DECLARE

    v_count NUMBER;

BEGIN


    -------------------------------------------------------
    -- ADD Z_UUID COLUMN
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME = 'Z_REPORT'
    AND COLUMN_NAME = 'Z_UUID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE Z_REPORT
        ADD Z_UUID VARCHAR2(32)
        ';

    END IF;



    -------------------------------------------------------
    -- POPULATE EXISTING RECORDS
    -------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    UPDATE Z_REPORT
    SET Z_UUID = RAWTOHEX(SYS_GUID())
    WHERE Z_UUID IS NULL
    ';



    -------------------------------------------------------
    -- MAKE NOT NULL
    -------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    ALTER TABLE Z_REPORT
    MODIFY Z_UUID NOT NULL
    ';



    -------------------------------------------------------
    -- UNIQUE CONSTRAINT
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_CONSTRAINTS
    WHERE TABLE_NAME = 'Z_REPORT'
    AND CONSTRAINT_NAME = 'UK_Z_REPORT_UUID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE Z_REPORT
        ADD CONSTRAINT UK_Z_REPORT_UUID
        UNIQUE(Z_UUID)
        ';

    END IF;



    -------------------------------------------------------
    -- SAFETY: ADD UPDATED_AT IF MISSING
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='Z_REPORT'
    AND COLUMN_NAME='UPDATED_AT';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE Z_REPORT
        ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ';

    END IF;


    COMMIT;


END;
/

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

-- PAYMENTS
ALTER TABLE PAYMENTS
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT PAYMENTS_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);
ALTER TABLE PAYMENTS
ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

-- RECEIPTS
ALTER TABLE RECEIPTS
ADD (
    KIOSKID VARCHAR2(32),
    CONSTRAINT RECEIPTS_FK_KIOSKID
        FOREIGN KEY (KIOSKID)
        REFERENCES KIOSK (KIOSKID)
);

UPDATE RECEIPTS SET KIOSKID = '55F3FEB197474ECAA84867CFB8CD8CC6' ;
ALTER TABLE RECEIPTS 
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

-- INVOICES
DECLARE

    v_count NUMBER;

BEGIN


    -------------------------------------------------------
    -- ADD UUID
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME = 'INVOICES'
    AND COLUMN_NAME = 'UUID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES
        ADD UUID VARCHAR2(32)
        ';

    END IF;



    -------------------------------------------------------
    -- POPULATE UUID
    -------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    UPDATE INVOICES
    SET UUID = RAWTOHEX(SYS_GUID())
    WHERE UUID IS NULL
    ';



    -------------------------------------------------------
    -- MAKE UUID NOT NULL
    -------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    ALTER TABLE INVOICES
    MODIFY UUID NOT NULL
    ';



    -------------------------------------------------------
    -- ADD UNIQUE CONSTRAINT UUID
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_CONSTRAINTS
    WHERE TABLE_NAME = 'INVOICES'
    AND CONSTRAINT_NAME = 'UK_INVOICES_UUID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES
        ADD CONSTRAINT UK_INVOICES_UUID
        UNIQUE(UUID)
        ';

    END IF;




    -------------------------------------------------------
    -- ADD KIOSKID
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='INVOICES'
    AND COLUMN_NAME='KIOSKID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES
        ADD KIOSKID VARCHAR2(32)
        ';

    END IF;



    -------------------------------------------------------
    -- POPULATE EXISTING KIOSKID
    -------------------------------------------------------
    EXECUTE IMMEDIATE
    '
    UPDATE INVOICES
    SET KIOSKID = ''55F3FEB197474ECAA84867CFB8CD8CC6''
    WHERE KIOSKID IS NULL
    ';



    -------------------------------------------------------
    -- ADD UPDATED_AT
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='INVOICES'
    AND COLUMN_NAME='UPDATED_AT';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES
        ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ';

    END IF;



    -------------------------------------------------------
    -- FK KIOSK
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_CONSTRAINTS
    WHERE TABLE_NAME='INVOICES'
    AND CONSTRAINT_NAME='INVOICES_FK_KIOSKID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES
        ADD CONSTRAINT INVOICES_FK_KIOSKID
        FOREIGN KEY(KIOSKID)
        REFERENCES KIOSK(KIOSKID)
        ';

    END IF;



    COMMIT;


END;
/

-- INVOICES_DET
DECLARE

    v_count NUMBER;

BEGIN


    -------------------------------------------------------
    -- ADD UUID
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='INVOICES_DET'
    AND COLUMN_NAME='UUID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES_DET
        ADD UUID VARCHAR2(32)
        ';

    END IF;



    -------------------------------------------------------
    -- ADD INV_UUID
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='INVOICES_DET'
    AND COLUMN_NAME='INV_UUID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES_DET
        ADD INV_UUID VARCHAR2(32)
        ';

    END IF;



    -------------------------------------------------------
    -- ADD PRODUCT_UUID
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='INVOICES_DET'
    AND COLUMN_NAME='PRODUCT_UUID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES_DET
        ADD PRODUCT_UUID VARCHAR2(32)
        ';

    END IF;



    -------------------------------------------------------
    -- ADD KIOSKID
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='INVOICES_DET'
    AND COLUMN_NAME='KIOSKID';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES_DET
        ADD KIOSKID VARCHAR2(32)
        ';

    END IF;



    -------------------------------------------------------
    -- ADD UPDATED_AT
    -------------------------------------------------------
    SELECT COUNT(*)
    INTO v_count
    FROM USER_TAB_COLUMNS
    WHERE TABLE_NAME='INVOICES_DET'
    AND COLUMN_NAME='UPDATED_AT';


    IF v_count = 0 THEN

        EXECUTE IMMEDIATE
        '
        ALTER TABLE INVOICES_DET
        ADD UPDATED_AT TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        ';

    END IF;


    COMMIT;


END;
/

UPDATE /*+ PARALLEL(4) */
INVOICES_DET
SET UUID = RAWTOHEX(SYS_GUID())
WHERE UUID IS NULL;

COMMIT;

MERGE /*+ PARALLEL(4) */
INTO INVOICES_DET d

USING
(
    SELECT
        SERNO,
        UUID,
        KIOSKID
    FROM INVOICES
) i

ON
(
    d.INV_SERNO = i.SERNO
)

WHEN MATCHED THEN UPDATE SET

    d.INV_UUID = i.UUID,
    d.KIOSKID = i.KIOSKID

WHERE d.INV_UUID IS NULL;

COMMIT;

MERGE /*+ PARALLEL(4) */
INTO INVOICES_DET d

USING
(
    SELECT
        SERNO,
        UUID
    FROM PRODUCTS
) p

ON
(
    d.INV_PR_SERNO = p.SERNO
)

WHEN MATCHED THEN UPDATE SET

    d.PRODUCT_UUID = p.UUID

WHERE d.PRODUCT_UUID IS NULL;

COMMIT;

CREATE INDEX IDX_INVDET_UUID
ON INVOICES_DET(UUID);


CREATE INDEX IDX_INVDET_INV_UUID
ON INVOICES_DET(INV_UUID);


CREATE INDEX IDX_INVDET_PRODUCT_UUID
ON INVOICES_DET(PRODUCT_UUID);


CREATE INDEX IDX_INVDET_SYNC
ON INVOICES_DET(KIOSKID, UPDATED_AT);

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