\ group-dictionary-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/group-dictionary.fs

CR .( group dictionary ) CR
.(   initially the group dictionnary is empty ) CR
T{
    GROUP-DICTIONARY-LENGTH@ 0 ?S
}T
.(   the group dictionary can be added word groups and their letters. ) CR
T{
    SS" horse" 0 UPDATE-NTH-GROUP
    GROUP-DICTIONARY-LENGTH@ 1 ?S
}T
.(   looking up the dictionary for a group gives its letters. ) CR
T{
    SS" horse" 0 NTH-GROUP
    GROUP-LETTERS@ S" h" ?STR
}T
.(   adding a word to the dictionary updates all its groups. ) CR
T{
    SS" mouse" ADD-WORD-GROUPS
    SS" house" ADD-WORD-GROUPS
    SS" horse" ADD-WORD-GROUPS
    SS" house" 0 NTH-GROUP GROUP-LETTERS@ S" hm" ?STR
    SS" house" 2 NTH-GROUP GROUP-LETTERS@ S" ru" ?STR
}T
.(   all the words from a file can be added to the dictionary. ) CR
T{
    s" ../data/www-cs-faculty.stanford.edu_~knuth_sgb-words.txt" READ-WORDS
    GROUP-DICTIONARY HCT-LENGTH@ 20859 ?S
    SS" house" 0 NTH-GROUP GROUP-LETTERS@ S" dhlmrsy" ?STR
    SS" paper" 1 NTH-GROUP GROUP-LETTERS@ S" ai" ?STR
    SS" start" 4 NTH-GROUP GROUP-LETTERS@ S" ekst" ?STR
}T
.(   the group dictionnary can check that a word is in the dictionary. ) CR
T{
    SS" paper" hex dbg WORD-EXIST? ?TRUE
    SS" fubar" WORD-EXIST? ?FALSE
}T
