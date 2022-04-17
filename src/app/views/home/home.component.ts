import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { ElementDialogComponent } from 'src/app/shared/element-dialog/element-dialog.component';

export interface PeriodicElement {
  nome: string;
  nomeHeroi: string;
  dataNascimento: any;
  altura: number;
  id: number;
  peso: number;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {id: 1, nome: 'Bruce Wayne', nomeHeroi: "Batman", dataNascimento: "02-04-1982", altura: 1.92, peso: 110.5,},
];

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  @ViewChild(MatTable)
  table!: MatTable<any>
  displayedColumns: string[] = ['id', 'nome', 'nomeHeroi', 'dataNascimento','altura', 'peso', 'actions'];
  dataSource = ELEMENT_DATA;

  constructor(public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog(element: PeriodicElement | null) : void {
    const dialogRef = this.dialog.open(ElementDialogComponent, {
      width: "250px",
      data: element === null ? {
        id: null,
        nome: "",
        nomeHeroi: "",
        dataNascimento: "",
        altura: null,
        peso: null,
      } : element
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result !== undefined){
        this.dataSource.push(result); 
        this.table.renderRows();
      }
    }); 
  }

  editarHeroi(element: PeriodicElement) : void {
    this.openDialog(element);
  }

  deleteHeroi(id: number) : void{
    this.dataSource = this.dataSource.filter(p => p.id !== id);
  }

}
