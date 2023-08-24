\ tests.fs
REQUIRE ffl/tst.fs
REQUIRE associative-array.fs
REQUIRE word-ladder.fs
PAGE

HEX
FFFF CONSTANT val1
1111 CONSTANT val2
AAAA CONSTANT val3

1001 CONSTANT key1
2002 CONSTANT key2
2003 CONSTANT key3
DECIMAL

T{ .( after creation assoc array is empty ) CR
    100 AA-CREATE my-assoc
    my-assoc AA-SIZE 0 ?S
    my-assoc AA-EMPTY? ?TRUE
    4807 my-assoc AA-FIND ?FALSE
}T


T{ .( after adding a key and value, array is not empty ) CR
    val1 key1 my-assoc AA-ADD
    my-assoc AA-SIZE 1 ?S
    my-assoc AA-EMPTY? ?FALSE
}T
T{ .( after adding a key and value, value can be found ) CR
    key1 my-assoc AA-FIND ?TRUE val1 ?S
}T
T{ .( after updating a value for a key, the value can be added ) CR
    key2 my-assoc AA-FIND ?FALSE
    val2 key2 my-assoc AA-UPDATE
    key2 my-assoc AA-FIND ?TRUE val2 ?S
    77777777 key2 my-assoc AA-UPDATE
    key2 my-assoc AA-FIND ?TRUE 77777777 ?S
    val2 key2 my-assoc AA-UPDATE
}T
T{ .( after inserting a value for a key, all other keys and values shift forward ) CR
    val3 key3 my-assoc AA-INSERT
    my-assoc AA-SIZE 3 ?S
    key3 my-assoc AA-FIND ?TRUE val3 ?S
}T

T{ .( find the nth key-value ) CR
    0 my-assoc AA-NTH key3 ?S val3 ?S
    1 my-assoc AA-NTH key1 ?S val1 ?S
    2 my-assoc AA-NTH key2 ?S val2 ?S
}T

1 AA-CREATE small
: TRY-ADD
    FALSE
    TRY
        2317 4807 small AA-ADD
        1001 4096 small AA-ADD
        IFERROR
            DROP TRUE
        THEN
    ENDTRY ;

T{ .( crossing the capacity limit should raise an execption ) CR
    TRY-ADD ?TRUE
    DROP
}T

VARIABLE accum
: add-kv ( v,k -- n )
    accum +!
    accum +! ;

T{ .( executing a word should execute a routine for each key/value ) CR
    accum OFF
    ' add-kv my-assoc AA-EXECUTE
    accum @ 134080 ?S 
}T

T{ .( storing a short string as a cell value ) CR
    S" FooBar" S>CELL 032195085310313990 ?S
    S" Foo" S>CELL 1869563395 ?S
    S" Bar" S>CELL 1918976515 ?S
    S" " S>CELL 0 ?S
    S"  " S>CELL 8193 ?S
    S"   " S>CELL 2105346 ?S
}T

50000 AA-CREATE my-list
T{ .( after adding a word, find the word ) CR
    S" DOG" my-list FIND-WORD ?FALSE
    S" DOG" my-list ADD-WORD
    S" DOG" my-list FIND-WORD ?TRUE 0 ?S
}T
T{ .( after updating a word, find the word was updated ) CR
    1 S" DOG" my-list UPDATE-WORD
    S" DOG" my-list FIND-WORD ?TRUE 1 ?S
}T
T{ .( after reading words from a file words can be found ) CR
    my-list AA-EMPTY
    S" ../../src/3-letter-words.txt" my-list READ-WORDS
    S" fog" my-list FIND-WORD ?TRUE DROP
    S" fubar" my-list FIND-WORD ?FALSE
}T
: .WL-WORD ( v,k -- )
    NIP PAD ! PAD COUNT TYPE SPACE ;

\ ' .WL-WORD my-list AA-EXECUTE
T{ .( two words are adjacent if they are the same size and differ by only one letter ) CR
    S" DOG" S" HORSE" ADJACENT? FALSE ?S
    S" DOG" S" FOG" ADJACENT? TRUE ?S
    S" DOG" S" DOG" ADJACENT? FALSE ?S
}T
100 AA-CREATE my-array

: .AA-VC 
    NIP COUNT TYPE CR ;

T{ .( after finding neighbors, array contains neigbors ) CR
    S" dog" my-list my-array NEIGHBORS
    my-array AA-SIZE 11 ?S
    0 my-array AA-NTH NIP COUNT s" bog" ?STR
    1 my-array AA-NTH NIP COUNT s" cog" ?STR

    S" DOG" my-list my-array NEIGHBORS
    my-array AA-SIZE 0 ?S
}T
BYE
T{ .( after creation associative array is empty ) CR
    256 AA-CREATE my-assoc
    my-assoc AA-EMPTY? TRUE ?S
}T
T{ .( after adding or update a key/value value can be found ) CR
    2317 4807 my-assoc AA-ADD-UPDATE
    4807 my-assoc AA-FIND TRUE ?S 2317 ?S
}T
T{ .( after emptying assoc array values cannot be found ) CR
    my-assoc AA-EMPTY
    4807 my-assoc AA-FIND FALSE ?S
}T
T{ .( after updating an assoc array key value value is repladed ) CR
    2317 4807 my-assoc AA-ADD-UPDATE
    4807 my-assoc AA-FIND TRUE ?S 2317 ?S
    1723 4807 my-assoc AA-ADD-UPDATE
    4807 my-assoc AA-FIND TRUE ?S 1723 ?S
}T

BYE
