\ group-dictionary-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/group-dictionary.fs

CR .( group dictionary ) CR
.(   the group dictionary can be added word groups and their letters. ) CR
T{
    CHAR h S" ~orse" UPDATE-GROUP
    CHAR m S" ~orse" UPDATE-GROUP
    S" ~orse" GROUP-LETTERS PAD LETTER-SET>S PAD COUNT s" hm" ?STR
}T
.(   adding a word to the dictionary updates all its groups. ) CR
T{
    s" mouse" ADD-WORD-GROUPS
    s" house" ADD-WORD-GROUPS
    s" horse" ADD-WORD-GROUPS
    s" ~ouse" GROUP-LETTERS PAD LETTER-SET>S PAD COUNT s" hm" ?STR
    s" ho~se" GROUP-LETTERS PAD LETTER-SET>S PAD COUNT s" ru" ?STR
}T
.(   all the words from a file can be added to the dictionary. ) CR
T{
    s" ../data/www-cs-faculty.stanford.edu_~knuth_sgb-words.txt" READ-WORDS
    GROUP-DICTIONARY HCT-LENGTH@ 20859 ?S
    s" ~ouse" GROUP-LETTERS PAD LETTER-SET>S PAD COUNT s" dhlmrsy" ?STR
}T
