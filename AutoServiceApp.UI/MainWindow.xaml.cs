using System.Windows;
using Data.Interfaces;

namespace AutoServiceApp.UI;
public partial class MainWindow:Window{
 private readonly IRepairRequestRepository _repo;
 public MainWindow(IRepairRequestRepository r){_repo=r;InitializeComponent();}
}
