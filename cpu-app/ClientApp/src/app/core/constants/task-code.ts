export function taskCode(statuscode: number): string {
  let textStatus;
  switch (statuscode) {
    case 2: {
      textStatus = 'Not Started';
      break;
    }
    case 3: {
      textStatus = 'In Progress';
      break;
    }
    case 4: {
      textStatus = 'Waiting';
      break;
    }
    case 7: {
      textStatus = 'Deferred';
      break;
    }
    case 5: {
      textStatus = 'Completed';
      break;
    }
    case 6: {
      textStatus = 'Cancelled';
      break;
    }
    default: {
      textStatus = 'No Status';
      break;
    }
  }
  return textStatus;
}
