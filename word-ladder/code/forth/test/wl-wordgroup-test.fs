\ wl-wordgroup-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-wordgroup.fs
REQUIRE ../src/wl-letterset.fs

CR .( word group ) CR

.(   a word belongs to as much groups as it has letters. ) CR
.(   for instance the 0th group of horse is ~orse, with letter h. ) CR
T{
    WW cold 1 W>GROUP WG c~ld ?S
    WW zero 0 W>GROUP WG ~ero ?S
    WW horse 0 W>GROUP>LETTER CHAR h ?S WG ~orse ?S
    WW fax 2 W>GROUP>LETTER CHAR x ?S WG fa~ ?S
}T
.(   a word group is distinct from a word. ) CR
T{
    WG ~at IS-WORD-GROUP? ?TRUE
    WW cat IS-WORD-GROUP? ?FALSE
}T
.(   the group index of a group can be found from the pos of the wildcard. ) CR
T{
    WG cast~le GROUP-INDEX 4 ?S
    WG i~le GROUP-INDEX 1 ?S
}T
.(   a nth group and a letter can be converted to the word it contains. ) CR
T{
    WG mo~se CHAR u GROUP>LETTER>W WW mouse ?S
    WG ~ax CHAR m GROUP>LETTER>W WW max ?S
}T
.(   a group dictionnary can be added word groups. ) CR
T{
    WL-GROUP-DICT g1
    WW mouse g1 WLGD-ADD-WORD
    WG mo~se g1 WLGD-LETTERS PAD LS>S PAD COUNT S" u" ?STR
    WG m~use g1 WLGD-LETTERS PAD LS>S PAD COUNT S" o" ?STR
    WG mous~ g1 WLGD-LETTERS PAD LS>S PAD COUNT S" e" ?STR
    WW house g1 WLGD-ADD-WORD
    WG ~ouse g1 WLGD-LETTERS PAD LS>S PAD COUNT S" hm" ?STR
    WW horse g1 WLGD-ADD-WORD
    WW morse g1 WLGD-ADD-WORD
    WW worse g1 WLGD-ADD-WORD
}T

