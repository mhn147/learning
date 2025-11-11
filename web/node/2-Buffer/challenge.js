const myBuffer = Buffer.alloc(3);

myBuffer[0] = 0x48;
myBuffer[1] = 0x69;
myBuffer[2] = 0x21;

console.log(myBuffer.toString("utf-8"));
