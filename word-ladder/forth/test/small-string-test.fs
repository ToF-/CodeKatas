\ small-string-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/small-string.fs

CR .( small string ) CR
.(   a string of length 5 or less can be stored as a cell value, a small string. ) CR
T{
    S" horse" SMALL-STRING
    SMALL-STRING-S S" horse" ?STR
}T
.(   a small string utlitity to create small strings litterals. ) CR
T{
    SS" paper" SMALL-STRING-S S" paper" ?STR
}T
.(   a small string can be modified by writing one of its chars. ) CR
T{
    SS" shark" CHAR p 4 ROT SMALL-STRING-C!
    SMALL-STRING-S S" sharp" ?STR
}T
.(   the nth char of a small string can be extracted. ) CR
T{
    SS" mouse" 2 SMALL-STRING-C@ CHAR u ?S
    SS" anvil" 4 SMALL-STRING-C@ CHAR l ?S
}T
.(   the length of a small string can be extracted. ) CR
T{
    SS" paper" SMALL-STRING-LENGTH@ 5 ?S
}T
