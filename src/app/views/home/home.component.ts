import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { PeriodicElement } from 'src/app/models/PeriodicElement';
import { ElementDialogComponent } from 'src/app/shared/element-dialog/element-dialog.component';
import { PeriodicElementService } from 'src/services/periodicElement.services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [PeriodicElementService]
})
export class HomeComponent implements OnInit {
  @ViewChild(MatTable)
  table!: MatTable<any>;
  displayedColumns: string[] = [
    'nome',
    'nomeHeroi',
    'dataNascimento',
    'altura',
    'peso',
    'actions',
  ];
  dataSource!: PeriodicElement[];

  constructor(
    public dialog: MatDialog,
    public periodicElementService: PeriodicElementService,
    ) {
      this.periodicElementService.getHeroi()
      .subscribe((data: PeriodicElement[]) => {
        console.log(data)
        this.dataSource = data;
      })
    }

  ngOnInit(): void {
    
  }

  openDialog(element: PeriodicElement | null): void {
    const dialogRef = this.dialog.open(ElementDialogComponent, {
      width: '250px',
      data:
        element === null
          ? {
              nome: '',
              nomeHeroi: '',
              dataNascimento: '',
              altura: null,
              peso: null,
            }
          : {
              id: element.id,
              nome: element.nome,
              nomeHeroi: element.nomeHeroi,
              dataNascimento: element.dataNascimento,
              altura: element.altura,
              peso: element.peso,
          },
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(result);
      if(result !== undefined){
        console.log(result);
        if(this.dataSource.map(p => p.id).includes(result.id)){
          this.periodicElementService.editHeroi(result)
          .subscribe((data: PeriodicElement) => {
            this.dataSource[result.id -1] = data;
            this.table.renderRows
          });
        } else{
          this.periodicElementService.createHeroi(result)
          .subscribe((data: PeriodicElement) => {
            this.dataSource.push(data);
            this.table.renderRows();
          })
        }
      }
    });
  }

  editarHeroi(element: PeriodicElement): void {
    this.openDialog(element);
  }

  deleteHeroi(id: number): void {
    this.periodicElementService.deletHeroi(id)
    .subscribe(() => {
      this.dataSource = this.dataSource.filter((p) => p.id !== id);
    })
    
  }
}
