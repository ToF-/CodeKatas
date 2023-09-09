\ group-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/group.fs

CR .( group ) CR

.(   a word belongs to as much groups as it has letters. ) CR
.(   the nth group of a word is this word with a wildcard at position n. ) CR
T{
    S" horse" 0 PAD S>GROUP PAD COUNT s" ~orse" ?STR
    S" again" 4 PAD S>GROUP PAD COUNT s" agai~" ?STR
}T
.(   the group index from a group is the position of the wildcard. ) CR
T{
    S" br~in" GROUP-INDEX 2 ?S
    S" merc~" GROUP-INDEX 4 ?S
}T
.(   a nth group and a char can be converted to the word it contains. ) CR
T{
    S" mo~se" CHAR u PAD GROUP-CHAR>S PAD COUNT S" mouse" ?STR
    S" merc~" CHAR y PAD GROUP-CHAR>S PAD COUNT S" mercy" ?STR
}T
