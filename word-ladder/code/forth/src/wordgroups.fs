\ wordgroups.fs
REQUIRE wl-wordgroup.fs
REQUIRE wl-env.fs

WL-GROUP-DICT GD

: MAIN
    NEXT-ARG GD WLGD-READ-WORDS
    GD .WLGD ;
MAIN BYE

