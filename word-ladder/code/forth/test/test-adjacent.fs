\ test-adjacent.fs 
REQUIRE ../src/adjacent.fs

CR .( adjacent words ) CR
T{ .(   two strings are adjacent if they are the same size and differ by one char only. ) CR
    s" dog" s" horse" ADJACENT? ?FALSE
    s" dog" s" fog"   ADJACENT? ?TRUE
    s" dog" s" bag"   ADJACENT? ?FALSE
    s" "    s" "      ADJACENT? ?FALSE
}T
