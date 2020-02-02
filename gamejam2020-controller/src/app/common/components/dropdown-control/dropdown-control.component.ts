import { ChangeDetectionStrategy, Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-dropdown-control',
  template: `
    <div ngbDropdown class="d-block" (openChange)="onOpenChanged($event)">
      <button class="btn btn-secondary"
              id="dropdownBasic1"
              ngbDropdownToggle
              type="button">{{ selectedValue || label }}</button>
      <div ngbDropdownMenu aria-labelledby="dropdownBasic1">
        <button type="button" ngbDropdownItem *ngFor="let item of items" (click)="onItemClicked(item.value)">{{item.label}}</button>
      </div>
    </div>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropdownControlComponent),
      multi: true
    }
  ]
})
export class DropdownControlComponent implements ControlValueAccessor {
  selectedValue: string = '';
  private _touched: boolean = false;

  @Input() items: {label: string, value: string}[] = [];
  @Input() label: string = 'Select type';

  onChange: any = () => {
  };
  onTouched: any = () => {
  };

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  writeValue(val: string): void {
    this.selectedValue = val;
  }

  onItemClicked(item: string) {
    this.onChange(item);
    this.selectedValue = item;
  }

  onOpenChanged(opened: boolean) {
    if (!opened) {
      if (!this._touched) {
        this.onTouched();
        this._touched = true;
      }
    }
  }
}
