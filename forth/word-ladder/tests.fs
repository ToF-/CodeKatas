require ffl/tst.fs
require wordladder.fs
PAGE

: BINARY 2 BASE ! ;

\ reducing a string to a word key:
\ each letter of the word occupies 5 bits, A=00001, B=00010, … Z=11010
\ the letter value 0 has a special meaning

.( converting a char to a letter value and vice versa ) CR
T{
    CHAR A C>LETTER-VALUE 1  ?S
    CHAR Z C>LETTER-VALUE 26 ?S
    8  LETTER-VALUE>C CHAR H ?S
    20 LETTER-VALUE>C CHAR T ?S
    
}T

\ forming a word key involve extracting all the chars from a string,
\ reversing them, converting them in letter values, then folding them
\ in single integer value by LSHIFTing and ORing.

.( reversing a string ) CR
T{
    S" TARTS" 2DUP S-REVERSE!  S" STRAT" ?STR
    S" YARD" 2DUP S-REVERSE!  S" DRAY" ?STR
}T
.( splitting a string into its chars ) CR
T{
    S" DOG" S-CHARS CHAR G ?S CHAR O ?S CHAR D ?S
    S" DART" S-CHARS CHAR T ?S CHAR R ?S CHAR A ?S CHAR D ?S
}T
.( creating a word key from a string ) CR
T{
    s" CAT" S>WORD-KEY BINARY 000110000110100 ?S DECIMAL
    s" AGAIN" S>WORD-KEY BINARY 0000100111000010100101110 ?S DECIMAL
}T
.( converting a word key into a string ) CR
T{
    PAD BINARY 000110000110100 DECIMAL WORD-KEY>S S" CAT" ?STR
    PAD BINARY 0000100111000010100101110 DECIMAL WORD-KEY>S S" AGAIN" ?STR
}T
BYE

