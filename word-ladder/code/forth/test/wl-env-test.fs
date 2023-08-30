\ wl-env-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-graph.fs
REQUIRE ../src/wl-env.fs

CR .( wl-env ) CR
.(   a word graph can be read from a file. ) CR
T{
    s\" echo \"bee\ncat\ndog\nwax\n\" >sample.txt" system
    WL-GRAPH G
    S" sample.txt" G WLG-READ-WORDS
    WW dog G WLG-HAS-WORD? ?TRUE
    WW fly G WLG-HAS-WORD? ?FALSE
    WW wax G WLG-HAS-WORD? ?TRUE

}T
