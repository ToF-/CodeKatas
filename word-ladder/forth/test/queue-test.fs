\ queue-test.fs

REQUIRE ffl/tst.fs
REQUIRE ../src/queue.fs

CR .( queue ) CR
.(   after adding an element the queue is not empty. ) CR
T{
    QUEUE q
    q Q-EMPTY? ?TRUE
    4807 q Q-APPEND
    q Q-EMPTY? ?FALSE
}T
.(   after appending elements the elements can be popped in a FIFO manner. ) CR
T{ 
    23 q Q-APPEND
    17 q Q-APPEND
    q Q-POP 4807 ?S
    q Q-POP 23 ?S
    q Q-POP 17 ?S
}T
.(   after emptying the queue, the queue is empty. ) CR
T{ 
    23 q Q-APPEND
    17 q Q-APPEND
    q Q-EMPTY
    q Q-EMPTY? ?TRUE
}T 
.(   the head of the queue can be accessed without popping it. ) CR
T{
    4807 q Q-APPEND
    q Q-HEAD@ 4807 ?S
    q Q-EMPTY? ?FALSE
}T

