\ wordgroups.fs
REQUIRE wl-wordgroup.fs
REQUIRE wl-dictionary.fs
REQUIRE wl-env.fs

WL-DICTIONARY D

: MAIN
    NEXT-ARG D WLD-READ-WORDS
    D .WL-DICTIONARY ;

MAIN BYE

