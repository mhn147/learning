const { Buffer, constants } = require("buffer");

console.log(constants);

const b = Buffer.alloc(2 * 2 ** 30); // 2 GiB

setInterval(() => {
  b.fill(0x22);
}, 5000);
