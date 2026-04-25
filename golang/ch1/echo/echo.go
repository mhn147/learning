// Echo1 prints its command-line arguments.
package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	fmt.Println(os.Args[1:])
	fmt.Println("***")
	fmt.Println(strings.Join(os.Args[1:], " "))
	fmt.Println("***")
	s, sep := "", ""
	for index, arg := range os.Args[1:] {
		s += sep + "os.Args[" + strconv.Itoa(int(index)) + "] = " + arg
		sep = "\n"
	}
	fmt.Println(s)
}
