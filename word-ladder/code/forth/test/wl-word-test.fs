\ wl-word-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/wl-word.fs

CR .( wl-word ) CR

.(   a wl word should be no longer than 7 and include only letters chars from a to z. ) CR
T{
    s" abracadabra" WL-CHECK-WORD ?FALSE
    s" horse" WL-CHECK-WORD ?TRUE
    s" Foo" WL-CHECK-WORD ?FALSE
    s" we try" WL-CHECK-WORD ?FALSE
}T

.(   a wl word is stored as a cell value and can be converted back to a string. ) CR
T{
     WL-WORD horse PAD WL-WORD>S PAD COUNT S" horse" ?STR
     S" mouse" S>WL-WORD PAD WL-WORD>S PAD COUNT S" mouse" ?STR
}T

