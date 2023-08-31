\ wl-wordgroup-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-wordgroup.fs

CR .( word group ) CR

.(   a word belongs to as much groups as it has letters. ) CR
.(   for instance the 0th group of horse is ~orse, with letter h. ) CR
T{
    WW cold 1 W>GROUP WG c~ld ?S
    WW zero 0 W>GROUP WG ~ero ?S
    WW horse 0 W>GROUP>LETTER CHAR h ?S WG ~orse ?S
    WW fax 2 W>GROUP>LETTER CHAR x ?S WG fa~ ?S
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
