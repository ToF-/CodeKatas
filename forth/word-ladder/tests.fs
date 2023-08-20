require ffl/tst.fs
require wordladder.fs
PAGE

: BINARY 2 BASE ! ;

\ reducing a string to a word key:
\ each letter of the word occupies 5 bits, A=00001, B=00010, … Z=11010
\ the letter value 31 has a special meaning of wildcard letter
\ to describe a neighbor group
\ the first 2 bits of the word key give it's number of letters:
\ 01: 3 letter word, 10: 4 letter word, 11:five letter word

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
    s" CAT" S>WORD-KEY BINARY 00011000011010001 ?S DECIMAL
    s" CART" S>WORD-KEY BINARY 0001100001100101010010 ?S DECIMAL
    s" AGAIN" S>WORD-KEY BINARY 10011100001010010111011 ?S DECIMAL
}T
.( converting a word key into a string ) CR
T{
    BINARY 00011000011010001 DECIMAL PAD WORD-KEY>S S" CAT" ?STR
    BINARY 10011100001010010111011 DECIMAL PAD WORD-KEY>S S" AGAIN" ?STR
}T
\ given a word key, we can find its neighbor groups
\ for example the word CARD has the neighbor groups :
\ _ARD, C_RD, CA_D, and CAR_
\ each neighbor group is a word key, and we can attach
\ letters that complement the word key to form a full word
\ for instance, since we have the word CARD, we know that
\ the neighbor group _CARD exist, with the letter C attached
\ when/if we see the word LARD, we can add the letter L to 
\ the neighbor group _ARD
.( extracting the nth letter from a word-key ) CR
T{ 
    S" DOG" S>WORD-KEY 0 WORD-KEY-LETTER 4 ?S
    S" DOG" S>WORD-KEY 2 WORD-KEY-LETTER 7 ?S
}T
.( masking a word-key with a wildcard ) CR
T{
    S" DOG" S>WORD-KEY 0 WILDCARD! PAD WORD-KEY>S S" _OG" ?STR
    S" DOG" S>WORD-KEY 1 WILDCARD! PAD WORD-KEY>S S" D_G" ?STR
    S" DOG" S>WORD-KEY 2 WILDCARD! PAD WORD-KEY>S S" DO_" ?STR
}T
\ to keep the letters of a neighbor group we can make bit set
\ a 26 bits cell can store numbers from 1 to 26
.( storing and retrieving numbers in a bitset cell ) CR
T{
    1 0 BIT>>BITSET BITSET-SIZE 1 ?S
    1 0 BIT>>BITSET 
    2 SWAP BIT>>BITSET
    17 SWAP BIT>>BITSET DUP BITSET-SIZE 3 ?S
    BITSET-BITS 17 ?S 2 ?S 1 ?S
}T
\ for each string given in the initial word list, we insert its
\ word-key and the word-keys of its neighbor groups and letters. That way the 
\ dictionnary allows for looking for neighbors
.( feeding the word-key dictionnary ) CR
T{
    S" DOG" S>WORD-KEY FIND-WORD-KEY 0 ?S
    S" DOG" S>WORD-KEY ADD-WORD-KEY
    S" FOG" S>WORD-KEY ADD-WORD-KEY
    S" DOG" S>WORD-KEY FIND-WORD-KEY ?TRUE 0 ?S
    S" FOG" S>WORD-KEY FIND-WORD-KEY ?TRUE 0 ?S

    S" _OG" S>WORD-KEY FIND-WORD-KEY ?TRUE 
    DUP BITSET-SIZE 2 ?S
    BITSET-BITS 6 ?S 4 ?S

    S" D_G" S>WORD-KEY FIND-WORD-KEY ?TRUE 
    DUP BITSET-SIZE 1 ?S
    BITSET-BITS 15 ?S
}T

BYE

