const EventEmitter = require("events");

class Emitter extends EventEmitter {}

const myE = new Emitter();

myE.on("foo", () => {
  console.log("Event foo occured.");
});

myE.emit("foo");

console.log("****");

class MyCustomerEmitter {
  events = [];

  on(event, fn) {
    const match = this.events.find((e) => e.event === event);
    if (match) {
      match.fns.push(fn);
    } else {
      this.events.push({ event, fns: [fn] });
    }
  }

  emit(event, ...args) {
    const match = this.events.find((e) => e.event === event);
    if (match) {
      match.fns.forEach((fn) => fn(args));
    }
  }
}

const myCustomE = new MyCustomerEmitter();

myCustomE.on("foo", () => {
  console.log("Custom Event Emitter FN1.");
});
myCustomE.on("foo", () => {
  console.log("Custom Event Emitter FN2.");
});
myCustomE.on("foo", (x) => {
  console.log("Custom Event Emitter FN3.", x);
});
myCustomE.on("bar", () => {
  console.log("Custom Event Emitter FN4.");
});

myCustomE.emit("foo", "some text");
myCustomE.emit("bar");

// ****** (other custome one https://www.freecodecamp.org/news/how-to-code-your-own-event-emitter-in-node-js-a-step-by-step-guide-e13b7e7908e1/)
class EventEmitter {
  listeners = {};

  addListener(eventName, fn) {
    this.listeners[eventName] = this.listeners[eventName] || [];
    this.listeners[eventName].push(fn);
    return this;
  }

  on(eventName, fn) {
    return this.addListener(eventName, fn);
  }

  once(eventName, fn) {
    this.listeners[eventName] = this.listeners[eventName] || [];
    const onceWrapper = () => {
      fn();
      this.off(eventName, onceWrapper);
    };
    this.listeners[eventName].push(onceWrapper);
    return this;
  }

  off(eventName, fn) {
    return this.removeListener(eventName, fn);
  }

  removeListener(eventName, fn) {
    let lis = this.listeners[eventName];
    if (!lis) return this;
    for (let i = lis.length; i > 0; i--) {
      if (lis[i] === fn) {
        lis.splice(i, 1);
        break;
      }
    }
    return this;
  }

  emit(eventName, ...args) {
    let fns = this.listeners[eventName];
    if (!fns) return false;
    fns.forEach((f) => {
      f(...args);
    });
    return true;
  }

  listenerCount(eventName) {
    let fns = this.listeners[eventName] || [];
    return fns.length;
  }

  rawListeners(eventName) {
    return this.listeners[eventName];
  }
}
