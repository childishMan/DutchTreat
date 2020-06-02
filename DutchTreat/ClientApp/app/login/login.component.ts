﻿import { Component } from "@angular/core"
import { DataService } from "../shared/dataService";
import { Router } from "@angular/router";

@Component({
    selector: "login",
    templateUrl: "login.component.html",
    styleUrls: []
})
export class Login {
    constructor(private data: DataService, private router: Router) { }

    public creds = {
        username: "",
        password: ""
    };

    public errorMessage: string;

    onLogin() {
        this.data.login(this.creds)
            .subscribe(
                success => {
                    if (success) {
                        if (this.data.order.items.length == 0) {
                            this.router.navigate([""]);
                        } else {
                            this.router.navigate(["checkout"]);
                        }
                    }
                }, err => this.errorMessage = "failed to login!"
            );
    };

}