\ test-queue.fs
REQUIRE ffl/tst.fs
REQUIRE ../src/queue.fs

CR
.( queue ) CR

T{ .(   after adding an element the queue is not empty. ) CR
    QUEUE q
    q Q-EMPTY? ?TRUE
    4807 q Q-APPEND
    q Q-EMPTY? ?FALSE
}T

T{ .(   after appending elements the elements can be popped in a FIFO manner. ) CR
    23 q Q-APPEND
    17 q Q-APPEND
    q Q-POP 4807 ?S
    q Q-POP 23 ?S
    q Q-POP 17 ?S

}T
