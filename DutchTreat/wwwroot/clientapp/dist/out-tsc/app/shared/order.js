import * as lo from "lodash";
export class Order {
    constructor() {
        this.orderDate = new Date();
        this.items = new Array();
    }
    get subtotal() {
        return lo.sum(lo.map(this.items, i => i.unitPrice * i.quantity));
    }
    ;
}
export class OrderItem {
}
//# sourceMappingURL=order.js.map