#[allow(dead_code)]
fn bubblesort_once(lst: &[u32]) -> Vec<u32> {
    if lst.len() < 2 {
        lst.to_vec()
    } else {
        let mut i = 1;
        let mut result = lst.to_vec();
        while i < result.len() {
            if result[i-1] > result[i] {
                let swap = result[i];
                result[i] = result[i-1];
                result[i-1] = swap;
            }
            i += 1; 
        }
        result.to_vec()
    }
}



// Add your tests here.
// See https://doc.rust-lang.org/stable/rust-by-example/testing/unit_testing.html

#[cfg(test)]
mod tests {
    use super::bubblesort_once;

    fn dotest(a: &[u32], expected: &[u32]) {
        let actual = bubblesort_once(a);
        assert!(actual == expected, 
                "With a = {a:?}\nExpected {expected:?} but got {actual:?}")
    }

    #[test]
    fn example_test() {
        dotest(&[9, 7, 5, 3, 1, 2, 4, 6, 8], &[7, 5, 3, 1, 2, 4, 6, 8, 9]);
    }
}
