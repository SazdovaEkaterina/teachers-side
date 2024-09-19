import { Component, Input } from '@angular/core';
import { IValidatorRule } from '../../models/validator-rule';

@Component({
  selector: 'app-validation-rules',
  templateUrl: './validation-rules.component.html',
  styleUrls: ['./validation-rules.component.scss'],
})
export class ValidationRulesComponent {
  @Input() field: string = '';
  @Input() validationRules: IValidatorRule[] = [];

  public isFulfilled(rule: IValidatorRule): boolean {
    var validationRule = this.validationRules.find(
      (validationRule) => validationRule.id === rule.id
    );
    return validationRule?.pattern.test(this.field) ?? false;
  }
}
