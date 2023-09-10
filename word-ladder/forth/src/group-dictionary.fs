\ group-dictionary.fs

REQUIRE ffl/hct.fs
REQUIRE small-string.fs

25000 CONSTANT GROUP-DICTIONARY-SIZE
GROUP-DICTIONARY-SIZE HCT-CREATE GROUP-DICTIONARY

: GROUP-DICTIONARY-LENGTH@ ( -- n )
    GROUP-DICTIONARY HCT-LENGTH@ ;

: UPDATE-NTH-GROUP ( ss,n -- )
    2DUP SMALL-STRING-C@ -ROT
    NTH-GROUP DUP
    SMALL-STRING-S
    GROUP-DICTIONARY HCT-GET 0= IF
        LS-EMPTY
    THEN
    ROT SWAP LS-ADD-CHAR
    SWAP SMALL-STRING-S 
    GROUP-DICTIONARY HCT-INSERT ;


\ : GROUP-LETTERS ( addr,l -- ls )
\     GROUP-DICTIONARY HCT-GET DROP ;
\ 
\ : ADD-WORD-GROUPS ( addr,l -- )
\     DUP 0 ?DO
\         2DUP I PAD S>GROUP
\         OVER I + C@ 
\         PAD COUNT UPDATE-GROUP
\     LOOP 2DROP ;
\ 
\ 100 CONSTANT MAX-LINE
\ CREATE LINE-BUFFER MAX-LINE ALLOT
\ 
\ : READ-WORDS ( addr,l -- )
\     R/O OPEN-FILE THROW
\     BEGIN
\         DUP LINE-BUFFER MAX-LINE ROT
\         READ-LINE THROW
\     WHILE
\         LINE-BUFFER SWAP ADD-WORD-GROUPS
\     REPEAT
\     DROP CLOSE-FILE THROW ;
\ 
