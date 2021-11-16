import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { MatSelectionList } from '@angular/material/list';
import { MatProgressBar, ProgressBarMode } from '@angular/material/progress-bar';
import { CommandTypeDTO } from 'src/api/CommandTypeDTO';
import { CommandWrapper } from 'src/impl/CommandWrapper';
import { asGuid } from 'src/utils';

@Component({
	selector: 'app-commands',
	templateUrl: './commands.component.html',
	styleUrls: ['./commands.component.scss']
})
export class CommandsComponent implements OnInit {

	@Input()
	commands!: MatSelectionList;
	@ViewChild('progress')
	progress!: MatProgressBar;

	constructor() { }

	ngOnInit(): void {
	}

	public executeCommand(ct: CommandTypeDTO): void {
		this.progress.mode = 'indeterminate' as ProgressBarMode;
		console.trace(ct);
				setTimeout(() => {
					const resElmt = document.getElementById('Result') as HTMLInputElement;
					if(resElmt)
						resElmt.value = '';
				});

		const cw = new CommandWrapper(undefined, asGuid(ct.Id));

		ct?.Parameters?.forEach(p => {
		});

		cw.executeCmd()
			.then(cmd => {
				setTimeout(() => {
					const resElmt = document.getElementById('Result') as HTMLInputElement;
					if (resElmt) {
						const res = new CommandWrapper(cmd, asGuid(ct.Id));
						resElmt.value = res.getArgument('Result') as string;
					}
				}, 2000);
			})
			.catch((error) => {
				console.error(error);
			})
			.finally(() => {
				setTimeout(()=>this.progress.mode = 'determinate' as ProgressBarMode, 2000);
			})
			;
	}

}
