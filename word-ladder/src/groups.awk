{
    line = $0
    for (i = 1; i <= length(line); i++) {
        print substr(line, 1, i-1) "_" substr(line, i+1) " " $0
    }
}


