{
    line = $0
    printf $0 " -- {"
    for (i = 1; i <= length(line); i++) {
        printf substr(line, 1, i-1) "_" substr(line, i+1);
        if ( i < length(line) ) {
            printf ",";
        } else {
            printf "};\n";
        }
    }
}


