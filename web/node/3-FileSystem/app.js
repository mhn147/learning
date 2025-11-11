const fs = require("fs/promises");

(async () => {
  try {
    const createFile = async (path) => {
      let fileExists;
      try {
        fileExists = await fs.open(path, "r");
        return console.log(`The file ${path} already exists`);
      } catch (e) {
        let newFileHandle;
        try {
          newFileHandle = await fs.open(path, "w");
        } catch (e) {
          console.error(e);
        }
        newFileHandle?.close();
      }
      fileExists?.close();
    };

    // commands
    const CREATE_FILE = "create a file";

    const commandFileHandle = await fs.open("./command.txt", "r");

    commandFileHandle.on("change", async () => {
      const { size } = await commandFileHandle.stat();
      const buff = Buffer.alloc(size);
      const offset = 0;
      const length = buff.byteLength;
      const position = 0;
      await commandFileHandle.read(buff, offset, length, position);

      const command = buff.toString("utf-8");

      // create a file:
      // create a file <path>
      if (command.includes(CREATE_FILE)) {
        const filePath = command.substring(CREATE_FILE.length + 1);
        await createFile(filePath);
      }
    });

    // file watcher
    const watcher = fs.watch("./command.txt");
    for await (const event of watcher) {
      if (event.eventType === "change") {
        commandFileHandle.emit("change");
      }
    }
  } finally {
    commandFileHandle.close();
  }
})();
