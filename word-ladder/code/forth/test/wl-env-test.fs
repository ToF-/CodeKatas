\ wl-env-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-env.fs
REQUIRE ../src/wl-wordgroup.fs
REQUIRE ../src/wl-dictionary.fs

CR .( wl-env ) CR
.(  a dictionary can be loaded with words and their word groups. ) CR
T{
    WL-DICTIONARY dct
    S" sample.txt" dct WLD-READ-WORDS
    WG ~ee dct WLD-LETTER-SET@ PAD LS>S PAD COUNT S" b" ?STR
    WW bee dct WLD-HAS-WORD? ?TRUE
}T
.(   arguments are checked in the graph before search. ) CR
T{
    : check-non-existing-word s" foo" dct CHECK-WORD ;
    ' check-non-existing-word catch [if] TRUE [then] ?TRUE
}T

