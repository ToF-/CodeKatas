\ group-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/small-string.fs
REQUIRE ../src/group.fs


CR .( group ) CR

.(   a small string belongs to as much groups as it has letters. ) CR
.(   the nth group of a small string is this small string with a wildcard at position n. ) CR
T{
    SS" horse" 0 NTH-GROUP SMALL-STRING-S S" ~orse" ?STR
    SS" stark" 2 NTH-GROUP SMALL-STRING-S S" st~rk" ?STR
}T
.(   the group index from a group is the position of the wildcard. ) CR
T{
    SS" brain" 3 NTH-GROUP GROUP-INDEX 3 ?S
    SS" mercy" 4 NTH-GROUP GROUP-INDEX 4 ?S
}T
.(   a nth group and a char can be used to form again the original small string ) CR
T{
    SS" mouse" 2 NTH-GROUP
    CHAR u SWAP GROUP>WORD SMALL-STRING-S S" mouse" ?STR
}T
