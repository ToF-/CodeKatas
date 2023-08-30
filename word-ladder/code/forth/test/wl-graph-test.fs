\ wl-word-graph-test.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/wl-graph.fs

CR .( wl-graph ) CR
.(   a word added to the graph has no predecessor at first. ) CR
T{
    WL-GRAPH wg
    WL-WORD horse
    DUP wg WLG-ADD-WORD
    wg WLG-PRED@ 0 ?S
    WL-WORD horse wg WLG-HAS-WORD? ?TRUE
}T
.(   a word can be set as predecessor of a word in the graph. ) CR
T{
    WL-WORD house WL-WORD horse wg WLG-PRED!
    WL-WORD horse wg WLG-PRED@  WL-WORD house ?S
}T
.(   a word can be set as the start of a path in the graph. ) CR
T{
    WL-WORD house wg WLG-START!
    WL-WORD house wg WLG-PRED@  WLG-START ?S
}T
.(   the adjacents words of a word that don't have a predecessor yet can be searched in the graph ) CR
.(   then these words are updated to have the word as predecessor. ) CR
T{
    wg ACT-CLEAR
    WW bat WW cab WW cat WW cot WW dab WW dog WW cog WW fog WW fox WW fly 
    : add-words 0 ?do wg WLG-ADD-WORD loop ;
    10 add-words
    WW cat wg WLG-SEARCH-ADJACENTS!
    WW cab wg WLG-PRED@ WW cat ?S
    WW bat wg WLG-PRED@ WW cat ?S
    WW cot wg WLG-PRED@ WW cat ?S
}T
.(   the words adjacent to a word can be added to a queue for further visit. ) CR
T{
    QUEUE ad
    WW cat ad wg WLG-QUEUE-ADJACENTS
    ad Q-POP WW cab ?S
    ad Q-POP WW bat ?S
    ad Q-POP WW cot ?S
}T
.(   after a path search the words forming the path all have a predecessor ) CR
.(   the path can be traversed with an executable routine ) CR 
T{
    WW cat WW dog ad wg WLG-SEARCH-PATH ?TRUE
    ' .WLG-ITEM wg ACT-EXECUTE
}T
