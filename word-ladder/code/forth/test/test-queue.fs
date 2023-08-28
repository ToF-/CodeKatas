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
T{ .(   the head of the queue can be accessed without popping it. ) CR
    q Q-HEAD@ 4807 ?S
    q Q-EMPTY? ?FALSE
}T

T{ .(   after appending elements the elements can be popped in a FIFO manner. ) CR
    23 q Q-APPEND
    17 q Q-APPEND
    q Q-POP 4807 ?S
    q Q-POP 23 ?S
    q Q-POP 17 ?S

}T

T{ .( after emptying the queue, the queue is empty. ) CR
    23 q Q-APPEND
    17 q Q-APPEND
    q Q-EMPTY
    q Q-EMPTY? ?TRUE
}T
