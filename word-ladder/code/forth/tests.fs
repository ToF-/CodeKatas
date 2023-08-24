\ tests.fs
REQUIRE ffl/tst.fs
REQUIRE associative-array.fs
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
BYE
REQUIRE word-ladder.fs
PAGE

T{ .( after creation wl is empty ) CR
    100000 WL-CREATE my-list
    my-list WL-EMPTY? TRUE ?S
}T
T{ .( when empty wl cannot find a word ) CR
    S" DOG" my-list WL-FIND FALSE ?S 
}T
T{ .( after adding a word, wl can find the word ) CR
    S" DOG" my-list  WL-ADD 
    S" DOG" my-list WL-FIND
    COUNT S" DOG" ?STR
}T
T{ .( after emptying wl cannot find any word ) CR
    my-list WL-EMPTY
    S" DOG" my-list WL-FIND FALSE ?S
}T
T{ .( after reading words from a file words can be found ) CR
    my-list WL-EMPTY
    S" ../../src/3-letter-words.txt" my-list WL-READ-FILE
    S" ../../src/4-letter-words.txt" my-list WL-READ-FILE
    S" ../../src/5-letter-words.txt" my-list WL-READ-FILE
    S" fog" my-list WL-FIND 0= FALSE ?S
    S" zooms" my-list WL-FIND 0= FALSE ?S 
    S" fubar" my-list WL-FIND FALSE ?S
}T
T{ .( two words are adjacent if they are the same size and differ by only one letter ) CR
    S" DOG" S" HORSE" ADJACENT? FALSE ?S
    S" DOG" S" FOG" ADJACENT? TRUE ?S
    S" DOG" S" DOG" ADJACENT? FALSE ?S
}T
T{ .( after creation ar is empty ) CR
    1000 AR-CREATE my-array
    my-array AR-EMPTY? TRUE ?S
}T
T{ .( after adding a value, array has one more value ) CR
    4807 my-array AR-ADD
    23 my-array AR-ADD
    17 my-array AR-ADD
    my-array CELL+ @ 3 ?S
}T
T{ .( after adding a value in array, value can be found ) CR
    4807 my-array AR-EXIST? TRUE ?S
    3256 my-array AR-EXIST? FALSE ?S
}T
T{ .( after emptying array, value cannot be found ) CR
    17 my-array AR-EXIST? TRUE ?S
    my-array AR-EMPTY
    17 my-array AR-EXIST? FALSE ?S
}T
T{ .( after finding neighbors, array contains neigbors ) CR
    S" dog" my-list my-array WL-NEIGHBORS
    my-array AR-SIZE 11 ?S
    S" evil" my-list my-array WL-NEIGHBORS
    my-array AR-SIZE 0 ?S
}T
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
