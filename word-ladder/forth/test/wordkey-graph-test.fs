\ wordkey-graph-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/wordkey-graph.fs

CR .( wordkey graph ) CR
.(   a wordkey with no predecessor can be added in the graph. ) CR
T{
    s" horse" ADD-START-WORD
    s" horse" PAD PREDECESSOR@>S PAD COUNT S" " ?STR
}T
.(   a wordkey with a predecessor can be added in the graph. ) CR
T{
    s" horse" s" worse" ADD-ADJACENT-WORDS
    s" worse" PAD PREDECESSOR@>S PAD COUNT S" horse" ?STR
}T
.(   the path from a word until the start word can be displayed. ) CR
T{
    s" worse" s" morse" ADD-ADJACENT-WORDS
    s" morse" .WORD-PATH
}T
