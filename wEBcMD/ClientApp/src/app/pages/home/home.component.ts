import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommandTypeDTO } from 'src/api/CommandTypeDTO';
import { GetCommandTypes } from 'src/impl/GetCommandTypes';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
	public commandTypes!: CommandTypeDTO[];
	public sideNavOpen: boolean = false;

	constructor(
		@Inject('BASE_URL')
		private readonly _baseUrl: string,
		private readonly _http: HttpClient,
	) {
		const wrapper = new GetCommandTypes();

		wrapper.execute()
			.then((res) => {
				this.commandTypes = res
				this.sideNavOpen = true;
			})
			.catch((error) =>
				console.error(error)
			);
	}

	ngOnInit(): void {
	}

}

