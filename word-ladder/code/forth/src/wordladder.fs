REQUIRE word-graph.fs
WORD-GRAPH WG
QUEUE Q
S" ../../src/3-letter-words.txt" WG WG-READ-WORDS
S" ../../src/4-letter-words.txt" WG WG-READ-WORDS
S" ../../src/5-letter-words.txt" WG WG-READ-WORDS
NEXT-ARG 2DUP WG WG-CHECK-WORD S>KEY
NEXT-ARG 2DUP WG WG-CHECK-WORD S>KEY
OVER Q WG WG-SEARCH-PATH
WG .WG-PATH
BYE
