import { Component, Input } from '@angular/core';
import { IValidatorRule } from '../../models/validator-rule';

@Component({
  selector: 'app-validation-rules',
  templateUrl: './validation-rules.component.html',
  styleUrls: ['./validation-rules.component.scss']
})
export class ValidationRulesComponent {
  @Input() field: string = "";
  @Input() validationRules: IValidatorRule[] = [];

  public isFulfilled(ruleId: number): boolean {
    var rule = this.validationRules.find(rule => rule.id === ruleId);
    return rule?.pattern.test(this.field) ?? false;
  }
}
