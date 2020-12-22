"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var age_group_1 = require("./age-group");
var AgeGroupManager = /** @class */ (function () {
    function AgeGroupManager() {
    }
    AgeGroupManager.getAgeGroup = function (value) {
        var group = "";
        if (value <= 12) {
            group = age_group_1.AgeGroup.Child;
        }
        else if (value > 12 && value <= 18) {
            group = age_group_1.AgeGroup.Adolescence;
        }
        else if (value > 18 && value <= 59) {
            group = age_group_1.AgeGroup.Adult;
        }
        else if (value > 59) {
            group = age_group_1.AgeGroup.SeniorAdult;
        }
        return group;
    };
    return AgeGroupManager;
}());
exports.AgeGroupManager = AgeGroupManager;
//# sourceMappingURL=age-group-manager.js.map